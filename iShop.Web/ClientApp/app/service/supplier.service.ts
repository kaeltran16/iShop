import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';



@Injectable()
export class SupplierService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }

  
 
     // get Categories
    getSuppliers() {
        return this.http.get(this.Url + 'api/Suppliers')
            .map(res => res.json());

    }


    
      

    


}