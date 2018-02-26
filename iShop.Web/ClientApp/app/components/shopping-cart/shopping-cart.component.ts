import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ProductService } from '../../service/product.service';
import { SharedService } from '../../service/shared-service';
@Component({
    selector: 'shopping-cart',
    templateUrl: './shopping-cart.component.html',
    styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent {
    carts: any[] = [];
    totalPrice:number=0;
    constructor(private route: ActivatedRoute,
        private productService: ProductService,
        private sharedService: SharedService,
        private router: Router) {
        this.carts = [];

       
        for (var i = 0; i < localStorage.length; ++i) {

            if (localStorage.key(i) !== "token") {
                let cart = JSON.parse(String(localStorage.getItem(String(localStorage.key(i)))));
              
                this.productService.getProduct(cart.productId).subscribe(p => {
                    this.carts.push({
                        product: p,
                        quantity: cart.quantity
                    });
                    this.totalPrice += cart.quantity * p.price;
                });
            
            }
               
           


        }
    }

    checkOut() {
        this.router.navigate(['/order']);
    }



    deleteItemCart(cart: any) {

        localStorage.removeItem(cart.product.id);
        let index = this.carts.indexOf(cart);
        this.totalPrice -= cart.product.price * cart.quantity;
        this.carts.splice(index, 1);
       
    }
}
