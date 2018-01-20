 
import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/map'

import * as _ from 'underscore';
import { PagerService } from '../../service/page.service';
import { ProductService } from '../../service/product.service';
@Component({
    
    selector: 'more-product',
    templateUrl: './more-product.component.html',
    styleUrls: ['./more-product.component.css']
})
export class MoreProductComponent implements OnInit {
    constructor(private http: Http, private pagerService: PagerService, private productService: ProductService ) { }

    // array of all items to be paged
    private allItems: any[];

    // pager object
    pager: any = {};

    // paged items
    pagedItems: any[];

    ngOnInit() {
        this.productService.getProducts().subscribe(p => {
                // set items to json response
                this.allItems = p;

                // initialize to page 1
                this.setPage(1);
            });
    }

    setPage(page: number) {
        if (page < 1 || page > this.pager.totalPages) {
            return;
        }

        // get pager object from service
        this.pager = this.pagerService.getPager(this.allItems.length, page);

        // get current page of items
        this.pagedItems = this.allItems.slice(this.pager.startIndex, this.pager.endIndex + 1);
    }

    

}
