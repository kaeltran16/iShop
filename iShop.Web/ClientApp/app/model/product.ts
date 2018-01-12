export interface Product {
    id: number;
//    Category: Category;

    price: number;
//    ImageUrl: string;
    title: string;
//    Info: string;
//    Stock: number;
//    ExpiredDate: Date;
//    AddedDate: Date;
//    Image: Image;
}


export interface Category {
    Id: number;
    Detail: string;
    Image: Image;
    Name: string;

}


export interface Image {
    Id: number;
    FileName: string;

}