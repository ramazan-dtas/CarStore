import {product} from './product';
export interface category{
    id: number,
    categoryName: string,
    products?: product[] 
}