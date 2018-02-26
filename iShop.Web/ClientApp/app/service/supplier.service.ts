import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { Supplier } from '../model/Supplier';



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

    createSuppliers(token: string,supplier:Supplier) {
        return this.http.post(this.Url + 'api/Suppliers',supplier,
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


    editSuppliers(token: string, supplier: Supplier) {
        return this.http.put(this.Url + 'api/Suppliers/' + supplier.id,
            JSON.stringify(supplier),
                ({
                    headers: {
                        'Content-Type': 'application/json',
                        //USE credentials mode
                        withCredentials: true,
                        'Authorization': 'Bearer ' + token
                    }
                }) as any
            )
            .map(res => res.json());

    }


    deleteSuppliers(token: string, supplierId:string) {
        return this.http.delete(this.Url + 'api/Suppliers/' + supplierId,
                
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