import { Component, OnInit } from '@angular/core';
import { UserService } from '../../service/user.service';
import { User } from "../../model/User";
import { ProductService } from '../../service/product.service';
import { Product } from "../../model/product";
import { SharedService } from '../../service/shared-service';


@Component({
    selector: 'order',
    templateUrl: './order.component.html',
    styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
    ngOnInit(): void {

        // info user 
        let token = localStorage.getItem("token");
        this.userService.info(token).subscribe(u => {
                this.user = u;
                this.logged = true;
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
        this.sharedService.changeTokenEmitted$.subscribe(user => this.logged = true);
    }
    totalPrice:number=0;
    logged:boolean=false;
    user:User;
    outline: boolean = true;
    city: string = "Đà Lạt";
    ward: string = "";
    district: string = "";
    name: string = "";
    telephone: string = "";
    carts: any[]=[];
    chooseAddress(outline:boolean) {
        this.outline = outline;
    }

    onclick($event:any) {
        $event._submitted = true;
    }
    constructor(private userService: UserService, private productService: ProductService, private sharedService: SharedService) {
       
    }
    
}
