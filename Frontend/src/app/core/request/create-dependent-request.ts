import { DependentRequest } from "./dependent-request";

export interface CreateDependentRequest extends DependentRequest{
    dependentsData: CreateDependentRequestData[];
}

export interface CreateDependentRequestData {
    name: string;
    age: number;
}