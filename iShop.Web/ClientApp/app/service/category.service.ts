import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { Category } from '../model/Category';



@Injectable()
export class CategoryService {
    Url: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.Url = baseUrl;
    }

  
 
     // get Categories
    getCategories() {
        return this.http.get(this.Url + 'api/Categories')
            .map(res => res.json());

    }

    //creaate
    createCategories(category:Category,token:string) {
        return this.http.post(this.Url + 'api/Categories', category,
                ({
                    headers: {
                        //USE credentials mode
//                        'Content-Type': 'application/json',
                        withCredentials: true,
                        'Authorization': 'Bearer ' + token
                    }
                }) as any)
            .map(res => res.json());

    }


    //creaate
    editCategories(category: Category, token: string) {
        return this.http.put(this.Url + 'api/Categories/'+category.id, JSON.stringify(category),
                ({
                    headers: {
                        //USE credentials mode
                                                'Content-Type': 'application/json',
                        withCredentials: true,
                        'Authorization': 'Bearer ' + token
                    }
                }) as any)
            .map(res => res.json());

    }

    //delete category

   deleteCategories(categoryId:string,token:string) {
       return this.http.delete(this.Url + 'api/Categories/' + categoryId,
                ({
                    headers: {
                        //USE credentials mode
                       
                        withCredentials: true,
                        'Authorization': 'Bearer ' + token
                    }
                }) as any)
            .map(res => res.json());

    } 

    


}