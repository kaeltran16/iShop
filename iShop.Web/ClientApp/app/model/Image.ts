import {Product} from "./Product";


export class Image {
    fileName: string;
    product: Product;
    id:string;

    constructor(fileName: string) { this.fileName = fileName; }
}