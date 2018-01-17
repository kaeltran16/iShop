import { Component, TemplateRef,OnInit,Input } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { trigger, transition, state, animate, style } from '@angular/animations';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ProductService } from '../../service/product.service';
import { SharedService } from '../../service/shared-service';

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
      
        this.repeat = 0;
        //  update total sent to navbar component
        this.sharedService.changeEmitted$.subscribe(info => {

            this.repeat = 0;
            this.repeat++;
            
            this.totalPrice = 0;
            this.totalQuantity = 0;
            if (this.repeat ===1) {
                for (var i = 0; i < localStorage.length; ++i) {
                    //get local storaage 
                    let carts = JSON.parse(String(localStorage.getItem(String(localStorage.key(i)))));
                    this.productService.getProduct(carts.idProduct).subscribe(p => {
                        //count total price and total quantity
                        this.totalPrice = this.totalPrice + carts.quantity * p.price;
                        this.totalQuantity += carts.quantity;


                    });


                }
                return;
            }
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
        private sharedService: SharedService
      
    ) {
       

       
    }
   


    exitLogin(isLogin: any) {
        
        if (isLogin.login) {
            this.modalRef.hide();
            this.userName = isLogin.userName;
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
   
}
