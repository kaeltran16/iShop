
import { ProductService } from '../../service/product.service';
import { Product } from "../../model/product";
import { ChangeDetectorRef, TemplateRef, Component, OnInit,Input } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
@Component({
    selector: 'list-product',
    templateUrl: './list-product.component.html',
    styleUrls: ['./list-product.component.css']
})
export class ListProductComponent {
    @Input('name') name: any;
    @Input('title') title: string;
    products: Product[]=[];
    bought:boolean=false;
    start: number = 0;
    end: number = 3;
    viewProduct: boolean = false;
    product: Product;
    modalRef: BsModalRef;


    constructor(private productService: ProductService) {
        this.productService.getProducts().subscribe(p => {
            this.products = this.transform(p, this.name);
          
        });
    }
  
    transform(items: any[], filter: string): any {
        if (!items || !filter) {
            return items;
        }

        filter = filter.toLowerCase();
        if (filter === "t" || filter === "o")
            return items.filter((p, i: any, ps: any) => {
                let categories = p.categories.filter((c: any) => c.short.toLowerCase().indexOf(filter) !== -1);

                if (categories.length) return true;
                return false;
            });
        return items.filter((p, i: any, ps: any) => {
            let categories = p.categories.filter((c: any) => c.name.toLowerCase().indexOf(filter) !== -1);

            if (categories.length) return true;
            return false;
        });
    }
   
    //next button
    next() {
        if (this.end < this.products.length) {
            this.end += 1;
            this.start += 1;
          
            if (this.end === this.products.length-1) {
                this.viewProduct = true;
            }
        }
    }
    //previous button
    pre() {
        if (this.start > -1) {
            this.start -= 1;
            this.end -= 1;
            if (this.end < this.products.length) this.viewProduct = false;
        }
    }

  
}
