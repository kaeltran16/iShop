import { Component,Input } from '@angular/core';
import { trigger, transition, state, animate, style, keyframes, useAnimation, query, animateChild, group, stagger } from '@angular/animations';
import { CategoryService } from '../../service/category.service';
import { Category} from "../../model/Category";
import { SharedService } from '../../service/shared-service';


@Component({
    selector: 'item-menu',
    templateUrl: './item-menu.component.html',
    styleUrls: ['./item-menu.component.css'],
    animations: [
       
        trigger('show', [

            transition(':enter', animate('2000ms ease-in', keyframes([
                style({ opacity: 0, transform: 'scale(0.7)', offset: 0 }),
                style({ opacity: 1, transform: 'scale(1)', offset: 0.5 }),

            ])))
            ,

            transition(':leave', animate('2000ms ease-out', keyframes([
                style({ opacity: 0, transform: 'scale(1)', offset: 0 }),
                style({ opacity: 1, transform: 'scale(0)', offset: 0.5 }),

            ])))

        ])
    ]
})
export class ItemMenuComponent {

    @Input('title') title: any;
    @Input('short') short:any;
    is: boolean = false;
    categories: Category[] = [];

    constructor(private categoryService: CategoryService, private sharedService: SharedService) {
        this.categoryService.getCategories().subscribe(c => {
            this.categories = c;
        });

        
    }
    hover() {
        this.is = true;
      
    }
    leave() {
        this.is = false;
    
    }
    click() {
        this.sharedService.emitChangeCategory(true); 
        this.is = false;
    }
    getCategories() {
        this.categoryService.getCategories().subscribe(c => {
            this.categories = c;
        });

    }
    
}
