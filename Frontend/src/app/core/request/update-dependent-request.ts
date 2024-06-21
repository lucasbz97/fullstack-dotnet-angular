import { DependentRequest } from "./dependent-request";

export interface UpdateDependentRequest extends DependentRequest {
    id?: number;
    age: number;
    name: string;
}