import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import {ShoppingCart} from "../model/ShoppingCart";
import { Cart} from "../model/Cart";
import { Headers, RequestOptions } from '@angular/http';
import { Order } from "../model/order";


@Injectable()
export class OrderService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }

  
 
     // get shoppingcart  with Id user
    getProduct(id: string) {
        return this.http.get(this.Url + '/api/ShoppingCarts/user/' + id)
            .map(res => res.json());

    }


    createOrder(userId: string, shipping: any = {}) {
        let orderItems: any[]=[];
        for (var i = 0; i < localStorage.length - 1; ++i) {
            //get local storage 
            if (localStorage.key(i) !== "token") {
                orderItems.push(JSON.parse(String(localStorage.getItem(String(localStorage.key(i))))));
            }


        }
      
        let order: Order = shipping ? new Order(userId, orderItems, shipping) : new Order(userId, orderItems);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

    
        return this.http.post(this.Url + '/api/ShoppingCarts/', JSON.stringify(order), options )
            .map(res => {
                console.log(order);
                return res.json();
            });

    }


}