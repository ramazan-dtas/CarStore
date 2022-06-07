import { category } from "./category";
export interface product {
    id:number,
    productName:string, 
    price:number,
    productionYear:number, 
    km:number, 
    description:string, 
    categoryId?: number,
    category?: category 
    images?: string
}