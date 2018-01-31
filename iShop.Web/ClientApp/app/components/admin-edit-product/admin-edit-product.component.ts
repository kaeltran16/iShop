import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
//import { CookieService } from 'ngx-cookie-service';
import { RequestOptions,Http } from '@angular/http';
import { ProductService } from "../../service/product.service";
import { Product} from "../../model/Product";
import { Image } from '../../model/Image';


@Component({
    selector: 'admin-edit-product',
    templateUrl: './admin-edit-product.component.html',
    styleUrls: ['./admin-edit-product.component.css']
})
export class AdminEditProductComponent implements OnInit {
    ngOnInit(): void {

        let categories: string[] = [];
        this.product.categories.forEach((t: any) => categories.push(t.id));
        console.log(this.product.supplierId);


        this.itemProduct = new Product(
//            this.itemProduct.id=this.product
            categories,
            this.product.summary,
            this.product.price,
            this.product.sku,
            this.product.name,
            this.product.supplierId,
            this.product.inventory.stock,
            this.product.expiredDate);

        this.itemProduct.id = this.product.id;
    }
   itemProduct:Product;
    @Output() onclick = new EventEmitter<boolean>();
    @Input('product') product: any;
    minDate = new Date(2017, 5, 10);
    maxDate = new Date(2018, 9, 15);
    imageEdit:Image;
  

    constructor(private productService: ProductService) {
     

    }

    editProduct() {
        this.onclick.emit(true);
        let token = localStorage.getItem("token");
        console.log(this.itemProduct);
        token ? this.productService.editProduct(this.itemProduct, token)
            .subscribe(c => { this.onclick.emit(true); }, err => console.log(err)) : alert("Bạn không đủ quyền để thao tác với việc này");
      
    }

   


}
