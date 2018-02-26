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
import { ImageService } from "../../service/image.service";


@Component({
    selector: "admin-create-product",
    templateUrl: "./admin-create-product.component.html",
    styleUrls: ["./admin-create-product.component.css"]
})
export class AdminCreateProductComponent implements OnInit {
    ngOnInit(): void {
        //suppliers
        let token = localStorage.getItem("token");
        token? this.supplierService.getSuppliers(token).subscribe(s => this.suppliers = s):alert("ban không đủ quyền để truy cập vào mục này");


        // category
        this.categoryService.getCategories().subscribe(c => {
            this.categoriesSelect = c;
            console.log(this.categoriesSelect);
        });
       // product
        this.itemProduct = new Product(
            [], "", 0, "cái", "", "", 0, new Date()
           
        );

        
//        console.log(this.itemProduct);
    } 

   

    itemProduct: Product;
    categoriesSelect: Category[];
    categories: Category[]=[];
    category: number;
    suppliers:Supplier[];
    image = new Image("/images/add_image.png");
    minDate = new Date(2017, 5, 10);
    maxDate = new Date(2018, 9, 15);
    imageEvent: any;
    choosed:boolean=false ;
    @Output() onclick = new EventEmitter<boolean>();
   


  

    constructor(private productService: ProductService,
        private categoryService: CategoryService,
        private supplierService: SupplierService,
        private imageService: ImageService) {
     

    }


    loadImage(event: any) {
      
        
        if (event) this.imageEvent = event;
        if (event.target.files && event.target.files[0]) {
            var reader = new FileReader();

            reader.onload = (event: any) => {
                this.image = new Image(event.target.result);
                this.choosed = true;
            }

            reader.readAsDataURL(event.target.files[0]);
        }
     
       
    }

 


    show() {
        if (!this.itemProduct.categories.find(c => c === this.categoriesSelect[this.category].id)) {
            this.categories.push(this.categoriesSelect[this.category]);
            this.itemProduct.categories.push(this.categoriesSelect[this.category].id);
            console.log(this.itemProduct);
        }
    }
    createProduct($event:any) {
      
        $event._submitted = true;

        if ($event.valid && this.choosed) {
            let token = localStorage.getItem("token");
            token
                ? this.productService.createProduct(this.itemProduct, token)
                    .subscribe(c => {
                        this.onclick.emit(c); 
                       
                        let token = localStorage.getItem("token");
                        token ? this.imageService.createImages(this.imageEvent, c.id, token) : alert("bạn chưa đủ quyền vào mục này");
                    },
                    err => console.log(err))
                : alert("Bạn không đủ quyền để thao tác với việc này");
        }


    }

   


}
