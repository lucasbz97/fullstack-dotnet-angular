import { DependentModel } from './dependent.model';
export interface UserModel {
    id: number;
    name: string;
    age: number;
    dependents: DependentModel[]
}