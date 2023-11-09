import { AdditionalData } from "./additional.data";
import { Category } from "./category";
import { ProductUser } from "./product.user";

export interface Product {
    productId: string,
    categoryId: string,
    name: string,
    introductoryPrice: number,
    dateAdded: Date,
    additionalData: any,
    isDisabled: boolean,
    category: Category | undefined,
    productUser: ProductUser[] | undefined
}
