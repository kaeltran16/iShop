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

  
 
     // get all order
    geOrders() {
        return this.http.get(this.Url + '/api/Orders/')
            .map(res => res.json());
    }



    //create
    createOrder(userId: string="") {
        let orderItems: any[]=[];
        for (var i = 0; i < localStorage.length; ++i) {
            //get local storage 
            if (localStorage.key(i) !== "token") {
                orderItems.push(JSON.parse(String(localStorage.getItem(String(localStorage.key(i))))));
            }


        }
        console.log(orderItems);
        let order: Order = new Order(userId, orderItems);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.Url + 'api/Orders/', JSON.stringify(order), options )
            .map(res => {
                   
                        for (var i = 0; i < localStorage.length; ++i) {
                            //get local storage 
                            if (localStorage.key(i) !== "token") {
                                console.log(String(localStorage.key(i)) + i);
                                localStorage.removeItem(String(localStorage.key(i)));
                                i--;
                            }
                        }
                
                

                   console.log(order);
                return res.json();
            },
                (err: any)=>console.log(err)
        );

    }


}