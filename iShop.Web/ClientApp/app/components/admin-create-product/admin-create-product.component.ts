import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";
//import { CookieService } from 'ngx-cookie-service';
import { RequestOptions,Http } from "@angular/http";
import { ProductService } from "../../service/product.service";
import { Product} from "../../model/Product";
import { Supplier } from "../../model/Supplier";
import { Image } from "../../model/Image";
import { Category } from "../../model/category";
import { CategoryService } from "../../service/category.service";
import { SupplierService } from "../../service/supplier.service";
@Component({
    selector: "admin-create-product",
    templateUrl: "./admin-create-product.component.html",
    styleUrls: ["./admin-create-product.component.css"]
})
export class AdminCreateProductComponent implements OnInit {
    ngOnInit(): void {
        //suppliers
        this.supplierService.getSuppliers().subscribe(s => this.suppliers = s);


        // category
        this.categoryService.getCategories().subscribe(c => {
            this.categoriesSelect = c;
            console.log(this.categoriesSelect);
        });
       // product
        this.itemProduct = new Product(
            [], "", 0, "cái", "", "", 0
           
        );
//        console.log(this.itemProduct);
    } 
    itemProduct: Product;
    categoriesSelect: Category[];
    categories: Category[]=[];
    category: number;
    suppliers:Supplier[];
    image = new Image("add_image.png");
    minDate = new Date(2017, 5, 10);
    maxDate = new Date(2018, 9, 15);

    @Output() onclick = new EventEmitter<boolean>();
   

//    fileChange(event: any) {
//        let fileList: FileList = event.target.files;
//        if (fileList.length > 0) {
//            let file: File = fileList[0];
//            let formData: FormData = new FormData();
//            formData.append('uploadFile', file, file.name);
//            let headers = new Headers();
//            /** No need to include Content-Type in Angular 4 */
//            headers.append('Content-Type', 'multipart/form-data');
//            headers.append('Accept', 'application/json');
//            let options = new RequestOptions(({ headers: headers }) as any);
//            this.http.post(`${this.apiEndPoint}`, formData, options)
//                .map(res => res.json())
//                .catch(error => Observable.throw(error))
//                .subscribe(
//                    data => console.log('success'),
//                    error => console.log(error)
//                )
            
//        }
//    }
  

    constructor(private productService: ProductService,
        private categoryService: CategoryService,
        private supplierService: SupplierService) {
     

    }


    show() {
        if (!this.itemProduct.categories.find(c => c === this.categoriesSelect[this.category].id)) {
            this.categories.push(this.categoriesSelect[this.category]);
            this.itemProduct.categories.push(this.categoriesSelect[this.category].id);
            console.log(this.itemProduct);
        }
//        this.categoriesSelect.splice(this.category, 1);
    }
    createProduct($event:any) {
        console.log($event);
        $event._submitted = true;
        if ($event.valid) {
            let token = localStorage.getItem("token");
            token
                ? this.productService.createProduct(this.itemProduct, token)
                .subscribe(c => { this.onclick.emit(true); }, err => console.log(err))
                : alert("Bạn không đủ quyền để thao tác với việc này");
        }

    }

   


}
