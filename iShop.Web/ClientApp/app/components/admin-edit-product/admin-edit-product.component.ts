import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
//import { CookieService } from 'ngx-cookie-service';
import { RequestOptions,Http } from '@angular/http';
import { ProductService } from "../../service/product.service";
import { Product} from "../../model/Product";



@Component({
    selector: 'admin-edit-product',
    templateUrl: './admin-edit-product.component.html',
    styleUrls: ['./admin-edit-product.component.css']
})
export class AdminEditProductComponent implements OnInit {
    ngOnInit(): void {


        this.itemProduct = this.product;

    }
   itemProduct:Product;
    @Output() onclick = new EventEmitter<boolean>();
    @Input('product') product: any;

    fileChange(event: any) {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();
            formData.append('uploadFile', file, file.name);
            let headers = new Headers();
            /** No need to include Content-Type in Angular 4 */
            headers.append('Content-Type', 'multipart/form-data');
            headers.append('Accept', 'application/json');
//            let options = new RequestOptions(({ headers: headers }) as any);
//            this.http.post(`${this.apiEndPoint}`, formData, options)
//                .map(res => res.json())
//                .catch(error => Observable.throw(error))
//                .subscribe(
//                    data => console.log('success'),
//                    error => console.log(error)
//                )
            
        }
    }
  

    constructor(private  productService:ProductService) {
     

    }

    editProduct() {
        this.onclick.emit(true);
        let token = localStorage.getItem("token");
        token ? this.productService.editProduct(this.itemProduct, token) : alert("Bạn không đủ quyền để thao tác với việc này");
      
    }

   


}
