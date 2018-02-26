import { Component, OnInit, Inject } from '@angular/core';
import { UserService } from '../../service/user.service';
import { DOCUMENT } from '@angular/platform-browser';
import { ProductService } from '../../service/product.service';
import { Shipping } from "../../model/shipping";
import { SharedService } from '../../service/shared-service';
import { OrderService } from '../../service/order.service';
import { ShippingService } from '../../service/shipping.service';
import { Router } from '@angular/router';

@Component({
    selector: 'order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
    ngOnInit(): void {

        // info user 
        let token = localStorage.getItem("token");
        if(token)
        this.userService.info(token).subscribe(u => {
                this.user = u;
                this.logged = true;
                this.outline = true;
       
            },
            err => this.logged = false

        );
      



        // load list product;
        for (var i = 0; i < localStorage.length; ++i) {
            //get local storage 
            if (localStorage.key(i) !== "token") {
                let carts = JSON.parse(String(localStorage.getItem(String(localStorage.key(i)))));
                this.productService.getProduct(carts.productId).subscribe(p => {
                    this.carts.push({ product: p, quantity: carts.quantity });
                    this.totalPrice += p.price * carts.quantity;
                });
            }


        }


        //update if token change 
        this.sharedService.changeTokenEmitted$.subscribe(user => {
            this.user = user;
            this.logged = true;
            this.outline = true;
        });
    }
    totalPrice:number=0;
    logged:boolean=false;
    user:any;
    outline: boolean = false;
    city: string = "Đà Lạt";
    ward: string = "";
    district: string = "";
    name: string = "";
    telephone: string = "";
    carts: any[]=[];
    chooseAddress(outline:boolean) {
        this.outline = outline;
    }

   
    constructor(private userService: UserService,
        private productService: ProductService,
        private sharedService: SharedService,
        private orderService: OrderService,
        private shippingService: ShippingService,
        private router: Router,
        @Inject(DOCUMENT) private document: Document

    ) {
       
    }


    book($event: any) {
        $event._submitted = true;

        if ($event.invalid&&this.outline===false) {
            return;
        }

       
        if (this.logged) {
          
            this.orderService.createOrder(this.user.userInfo.id).subscribe(o => {
                 let shipping: Shipping;
                 if (this.outline)
                     shipping = new Shipping(0,
                         this.totalPrice,
                         this.user.userInfo.ward,
                         this.user.userInfo.district,
                         this.user.userInfo.city,
                         this.user.userInfo.firstName + " " + this.user.userInfo.lastName,
                         this.user.userInfo.phoneNumber,
                         o.id);
                 else {
                     shipping = new Shipping(0,
                         this.totalPrice,
                         this.ward,
                         this.district,
                         "Đà Lạt",
                         this.name,
                         this.telephone,
                         o.id);
                 }
                 this.shippingService.createShipping(shipping).subscribe(p => { this.router.navigate(['/home']); }
                     ,
                     err => console.log(err));
             },
                 (err: any) => console.log(err));
          
            } else {
                this.orderService.createOrder().subscribe(o => {
                    let shipping: Shipping;
                        if (this.outline)
                            shipping = new Shipping(0,
                                this.totalPrice,
                                this.user.userInfo.ward,
                                this.user.userInfo.district,
                                this.user.userInfo.city,
                                this.user.userInfo.firstName + " " + this.user.userInfo.lastName,
                                this.user.userInfo.phoneNumber,
                               o.id);
                        else {
                            shipping = new Shipping(0,
                                this.totalPrice,
                                this.ward,
                                this.district,
                                "Đà Lạt",
                                this.name,
                                this.telephone,
                                o.id);
                        }
                        this.shippingService.createShipping(shipping).subscribe(p => { this.router.navigate(['/home']); },
                            err => console.log(err));
                    }
                    );
            
         
          
            this.document.body.scrollTop = 0;

        }


    }
    
}
