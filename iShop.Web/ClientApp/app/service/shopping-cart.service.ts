import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }

    //get all product 
    getProducts() {
        return this.http.get(this.Url + '/api/Products/')
            .map(res => res.json());

    }
     // get product with Id 
    getProduct(id: string) {
        return this.http.get(this.Url + '/api/Product/' + id)
            .map(res => res.json());

    }


}