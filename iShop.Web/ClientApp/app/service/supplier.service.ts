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
    getSuppliers(token:string) {
        return this.http.get(this.Url + 'api/Suppliers',
                ({
                    headers: {
                        //USE credentials mode
                        withCredentials: true,
                        'Authorization': 'Bearer ' + token
                    }
                }) as any
        )
            .map(res => res.json());

    }


    
      

    


}