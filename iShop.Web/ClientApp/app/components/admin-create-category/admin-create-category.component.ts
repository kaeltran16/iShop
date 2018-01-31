import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";

import { CategoryService } from "../../service/category.service";

import { Category } from "../../model/Category";


@Component({
    selector: "admin-create-category",
    templateUrl: "./admin-create-category.component.html",
    styleUrls: ["./admin-create-category.component.css"]
})
export class AdminCreatecCategoryComponent implements OnInit {
    ngOnInit(): void {

        this.category.short = "-1";

    } 

 
   category: Category = new Category();
    @Output() onclick = new EventEmitter<boolean>();
    shorts: any[] = [{ value: "t", name: "Thịt và Trứng" },
        {
            value:"o",name:"Thủy Hải Sản"
        }];


    createCategory($event: any) {
        $event._submitted = true;
        console.log(this.category);
        if ($event.valid) {
            let token = localStorage.getItem("token");
            token
                ? this.categoryService.createCategories(this.category, token).subscribe(c => this.onclick.emit(true),err=> console.log(err)):alert("Bạn không đủ quyền vào mục này");
        }
    }


    constructor(
        private categoryService: CategoryService,
        ) {
     

    }


  

   


}
