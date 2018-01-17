export class Cart {
    idProduct: string;
    quantity: number;
    constructor(idProduct:string, quantity:number) {
        this.idProduct = idProduct;
        this.quantity = quantity;
    }
}