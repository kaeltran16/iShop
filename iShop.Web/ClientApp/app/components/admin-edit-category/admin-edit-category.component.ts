import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";

import { CategoryService } from "../../service/category.service";

import { Category } from "../../model/Category";


@Component({
    selector: "admin-edit-category",
    templateUrl: "./admin-edit-category.component.html",
    styleUrls: ["./admin-edit-category.component.css"]
})
export class AdminEditcCategoryComponent implements OnInit {
    ngOnInit(): void {

       

    } 

    constructor(
        private categoryService: CategoryService,
    ) {
    }
 
   @Input("category")category: Category = new Category();
    @Output() onclick = new EventEmitter<boolean>();
    shorts: any[] = [{ value: "t", name: "Thịt và Trứng" },
        {
            value:"o",name:"Thủy Hải Sản"
        }];


    editCategories($event: any) {
        $event._submitted = true;
       
        if ($event.valid) {
            let token = localStorage.getItem("token");
            token
                ? this.categoryService.editCategories(this.category, token)
                    .subscribe(c => this.onclick.emit(true), err => console.log(err)) : alert("Bạn không đủ quyền vào mục này");
        }
    }

    deleteCategories() {
     
            let token = localStorage.getItem("token");
            token
                ? this.categoryService.deleteCategories(this.category.id, token)
                    .subscribe(c => this.onclick.emit(true), err => console.log(err)) : alert("Bạn không đủ quyền vào mục này");
        
    }


   


  

   


}
