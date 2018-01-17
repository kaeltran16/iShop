
import { Category } from "./Category";
import { Image } from "./Image";
import { Supplier } from "./Supplier";
export class Product {
    id: number;
    category: Category;
    summary: string;
    expiredDate: Date;
    addedDate:Date;
    price: number;
    sku:string;
    name: string;
    supplier:Supplier;
    image: Image;
}


