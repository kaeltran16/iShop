import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

import { Headers, RequestOptions } from '@angular/http';
import { Shipping } from "../model/shipping";


@Injectable()
export class ShippingService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }

  
 
     // get shoppingcart  with Id user
   


    createShipping(shipping:Shipping) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
     
        return this.http.post(this.Url + 'api/shippings/', JSON.stringify(shipping), options )
            .map(res => res.json(),
                (err: any)=>console.log(err)
        );

    }


    updateShipping(shipping: Shipping,token:string) {
      

        return this.http.put(this.Url + 'api/Shippings/' + shipping.id, shipping,
             
                ({
                headers: {
                    //USE credentials mode
                  
                    withCredentials: true,
                    'Authorization': 'Bearer ' + token
                }
            }) as any)
            .map(res => res.json()
            );
    }

}