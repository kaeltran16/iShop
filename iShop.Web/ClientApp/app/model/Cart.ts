export class Cart {
    productId: string;
    quantity: number;
    constructor(idProduct:string, quantity:number) {
        this.productId = idProduct;
        this.quantity = quantity;
    }
}