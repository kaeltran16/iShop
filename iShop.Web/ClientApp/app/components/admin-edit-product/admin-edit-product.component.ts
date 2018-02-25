import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
//import { CookieService } from 'ngx-cookie-service';
import { RequestOptions,Http } from '@angular/http';
import { ProductService } from "../../service/product.service";
import { Product} from "../../model/Product";
import { Image } from '../../model/Image';
import { ImageService } from '../../service/image.service';


@Component({
    selector: 'admin-edit-product',
    templateUrl: './admin-edit-product.component.html',
    styleUrls: ['./admin-edit-product.component.css']
})
export class AdminEditProductComponent implements OnInit {
    ngOnInit(): void {

        let categories: string[] = [];
        this.product.categories.forEach((t: any) => categories.push(t.id));
       


        this.itemProduct = new Product(
//            this.itemProduct.id=this.product
            categories,
            this.product.summary,
            this.product.price,
            this.product.sku,
            this.product.name,
            this.product.supplier.id,
            this.product.inventory.stock,
            new Date(this.product.expiredDate) );

        this.itemProduct.id = this.product.id;
      
    }
   itemProduct:Product;
   @Output('onclick') onclick = new EventEmitter<any>();
    @Input('product') product: any;
    minDate = new Date(2018, 2, 1);
    maxDate = new Date(2020, 12, 30);
    imageEdit:Image;
  

    constructor(private productService: ProductService,
    private  imageService:ImageService) {
     

    }

    editProduct() {
    
        let token = localStorage.getItem("token");
    
        token ? this.productService.editProduct(this.itemProduct, token)
            .subscribe(c => {
                this.onclick.emit(c);

            }, err => console.log(err)) : alert("Bạn không đủ quyền để thao tác với việc này");
      
    }
    deleteProduct() {
   
        let token = localStorage.getItem("token");
       
        token ? this.productService.deleteProduct(this.itemProduct.id, token)
            .subscribe(c => {
                this.onclick.emit(true);
                if (token) this.imageService.deleteImage(this.product.images[0].fileName, this.product.id, token)
                    .subscribe(data => console.log(data),
                err=>console.log(err));
            }, err => console.log(err)) 
              : alert("Bạn không đủ quyền để thao tác với việc này");

    }
   


}
