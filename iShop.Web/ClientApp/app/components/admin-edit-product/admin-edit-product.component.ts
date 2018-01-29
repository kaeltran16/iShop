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

//        this.itemProduct = this.product;
//        this.imageEdit = this.product.images[0];
//        this.itemProduct.sku = this.product.sku;
//        this.itemProduct.name = "vu khac hoi";
//        this.itemProduct.price = this.product.price;
//        this.itemProduct.summary = this.product.summary;
//        this.itemProduct.stock = this.product.stock;
//        this.itemProduct.supplierId = this.product.supplierId;
//        this.itemProduct.categories = this.product.categories;
//        this.itemProduct.stock = this.product.inventory.stock;


        this.itemProduct=new Product(this.product.categories,this.product.summary,this.product.price,this.product.sku,this.product.name,this.product.supplier,this.product.stock,this.product.expiredDate)
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
