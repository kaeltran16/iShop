 
import { Component, OnInit, Inject  } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, ParamMap } from '@angular/router';
import 'rxjs/add/operator/map'
import { SharedService } from '../../service/shared-service';
import { DOCUMENT } from '@angular/platform-browser';
import { PagerService } from '../../service/page.service';
import { ProductService } from '../../service/product.service';
@Component({
    
    selector: 'more-product',
    templateUrl: './more-product.component.html',
    styleUrls: ['./more-product.component.css']
})
export class MoreProductComponent implements OnInit {
    constructor(private http: Http,
        private pagerService: PagerService,
        private productService: ProductService,
        private route: ActivatedRoute,
        private sharedService: SharedService,
        @Inject(DOCUMENT) private document: Document
    ) { }
   
    // array of all items to be paged
    private allItems: any[];
    category: string | null;
    title:string|null;
    // pager object
    pager: any = {};

    // paged items
    pagedItems: any[];

    ngOnInit() {
        //set position on top
        this.document.body.scrollTop = 0;

        this.productService.getProducts().subscribe(p => {
            // set items to json response
            this.allItems = p;
            this.category = this.route.snapshot.paramMap.get('title');
            if (this.category === 'o') this.title = "Thủy Hải Sản";
            else if (this.category === 't') this.title = "Thịt Và Trứng";
            else this.title = this.category;
            // initialize to page 1
            this.setPage(1);
        });
        this.sharedService.changeCategoryEmitted$.subscribe(info => {
            
            this.productService.getProducts().subscribe(p => {
                // set items to json response
                this.allItems = p;
                this.category = this.route.snapshot.paramMap.get('title');
                if (this.category === 'o') this.title = "Thủy Hải Sản";
                else if (this.category === 't') this.title = "Thịt Và Trứng";
                else this.title = this.category;
                // initialize to page 1
                this.setPage(1);
            });
        });
       
    }

    setPage(page: number) {
        if (page < 1 || page > this.pager.totalPages) {
            return;
        }

        // get pager object from service
        this.pager = this.pagerService.getPager(this.allItems.length, page,15);

        // get current page of items
        this.pagedItems = this.allItems.slice(this.pager.startIndex, this.pager.endIndex + 1);
    }

    

}
