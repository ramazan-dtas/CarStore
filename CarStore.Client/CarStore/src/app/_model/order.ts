import { user } from './user';

export interface order {
    id : number,
    orderDateTime : Date,
    userId : number,
    user? : user 
}