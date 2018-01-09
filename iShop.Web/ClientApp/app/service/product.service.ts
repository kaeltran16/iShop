import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }
    getProducts() {
        return this.http.get(this.Url + '/api/Product/')
            .map(res => res.json());

    }

    getProduct(id: string) {
        return this.http.get(this.Url + '/api/Product/' + id)
            .map(res => res.json());

    }


}