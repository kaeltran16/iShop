
import { ProductService } from '../../service/product.service';
import { Product } from "../../model/product";
import { Component, Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes } from '@angular/animations';
@Component({
    selector: 'list-product-random',
    templateUrl: './list-product-random.component.html',
    styleUrls: ['./list-product-random.component.css'],
    animations: [
        trigger('slideInOut', [
            state('in', style({
                'max-height': '500px', 'opacity': '1',
            })),
            state('out', style({

            })),
            transition('in => out', [
                    animate('2000ms ease-in', keyframes([
                        style({ opacity: 0, transform: 'scale(1)', offset: 0 }),
                        style({ opacity: 1, transform: 'scale(2)', offset: 0.5 }),

                    ]))

                ]
            ),
            transition('out => in', [
                    animate('400ms ease-in-out', style({
                        transform: 'translateX(-100%)'
                    }))
                ]
            )])]


})
export class ListProductRandomComponent {
    @Input('name') name: any;
    @Input('title') title: string;
    products: Product[]=[];
    bought:boolean=false;
    start: number = 0;
    end: number = 3;
    viewProduct: boolean = false;
    product: Product;
    isExpanded:boolean=false;


    constructor(private productService: ProductService) {
        this.productService.getProducts().subscribe((p) => {
            
            for (var i = 0; i < p.length; i++) {
                let index = Math.floor(Math.random() * (p.length));
                this.products.push(p[index]);
                p.splice(index,1);
            }
        });

    }
    animationState = 'in';
    toggleShowDiv(divName: string) {
        if (divName === 'divA') {

            this.animationState = this.animationState === 'out' ? 'in' : 'out';
            console.log(this.animationState);
        }
    }
    tongle() {
        this.isExpanded = !this.isExpanded;
    }
   
    //next button
    next() {
        if (this.end < 10) {
            this.start += 1;
            this.end += 1;
            if (this.end === 9) {
                this.viewProduct = true;
            }
        }
    }
    //previous button
    pre() {
        if (this.start > 0) {
            this.start -= 1;
            this.end -= 1;
        }
    }

  
}
