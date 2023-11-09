export interface UserOp{
    name: string,
    surname: string,
    userName: string,
    id: string,
    isDisabled: boolean,
    loyaltyCardCode: string,
    fiscalCode: string,
    userRoles: string[] | null
}
