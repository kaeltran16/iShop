import { Component, TemplateRef,OnInit,Input } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { trigger, transition, state, animate, style } from '@angular/animations';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ProductService } from '../../service/product.service';
import { SharedService } from '../../service/shared-service';
import { UserService } from '../../service/user.service';
@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    animations: [
        trigger('myAwesomeAnimation', [
            state('small', style({
                transform: 'scale(0.7)',
            })),
            state('large', style({
                transform: 'scale(1)',
            })),
            transition('small <=> large', animate('2000ms ease-in'))
        ]),
       
    ]

})
export class NavMenuComponent implements OnInit{
    ngOnInit() {
      
   
        //  update total sent to navbar component
        this.sharedService.changeEmitted$.subscribe(info => {

            setTimeout(() => {
                    this.totalPrice = 0;
                    this.totalQuantity = 0;
                
                    for (var i = 0; i < localStorage.length; ++i) {
                        //get local storage 
                        if (localStorage.key(i) !== "token") {
                            let carts = JSON.parse(String(localStorage.getItem(String(localStorage.key(i)))));
                            this.productService.getProduct(carts.productId).subscribe(p => {
                                //count total price and total quantity
                                this.totalPrice = this.totalPrice + carts.quantity * p.price;
                                this.totalQuantity += carts.quantity;


                            });
                        }


                    }
                },
                1000);


        });
    }
    repeat:number=0;
    totalPrice: number=0;
    totalQuantity:number=0;
    carts: any[] = [];
    @Input('infoCart') infoCart:any;
    userName:string="Đăng nhập";
    isShow: boolean = false;
    logged: boolean = false;
   
    meet: any[] = [
        "Thịt Heo",
        "Thịt Bò",
        "Thịt Gà và Trứng"
    ];
    seaFood: any[] = [
        "Cá Đồng",
        "Cá Biển",
        "Các Loại Thủy Hải Sản Khác"
    ];

    modalRef: BsModalRef;
    constructor(private modalService: BsModalService,
        private route: ActivatedRoute,
        private router: Router,
        private productService: ProductService,
        private sharedService: SharedService,
        private userService:UserService

      
    ) {
        // get current user when token valid
        let currentToken = localStorage.getItem("token");
        if (currentToken)
           
                this.userService.info(currentToken).subscribe(user => {
                    this.userName = user.lastName.toUpperCase();
                    this.logged = true;
                },
                    err => {
                        this.logged = false;
//                         localStorage.removeItem("token");
                    });
          
        // when token change ,  update user 
        this.sharedService.changeTokenEmitted$.subscribe(user => this.userName = user.lastName.toUpperCase());
    }
   


    exitLogin(isLogin: boolean) {
        
        if (isLogin) {
            this.modalRef.hide();
         
            this.logged = true;
        }
        
    }
    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template);
       
    }

    openShoppingCart() {
//        this.router.navigate(['/shopping-cart']);
        this.isShow = true;
        this.carts = [];
      
    

    }
    closeShoppingCart() {
      
        this.isShow = false;
    }
    // logout
    logout() {
        let token = localStorage.getItem("token");
        this.userService.logout(token);
        this.logged = false;
        this.userName = "Đăng nhập";
        localStorage.removeItem("token");
    }
   
}
