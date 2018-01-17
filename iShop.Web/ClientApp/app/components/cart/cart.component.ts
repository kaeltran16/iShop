import { Component, Input, OnInit } from '@angular/core';
import { ProductService } from '../../service/product.service';
import { SharedService } from '../../service/shared-service';
@Component({
    selector: 'cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
   
  a:number=0;
   carts:any[]=[];
    ngOnInit(): void {

        
    }
    constructor(private productService: ProductService, private _sharedService: SharedService) {
        //        get local storage
        
        this._sharedService.changeEmitted$.subscribe(info => {
            this.carts = [];
            this.a = localStorage.length;
            if (this.carts.length < localStorage.length) {
                for (var i = 0; i < localStorage.length; ++i) {
                    
                    let cart = JSON.parse(String(localStorage.getItem(String(localStorage.key(i)))));
                    this.productService.getProduct(cart.idProduct).subscribe(p => {
                        this.carts.push({
                           
                            product: p,
                            quantity: cart.quantity
                        });

                    });


                }
            }
           
        });
       
    }
  


}
