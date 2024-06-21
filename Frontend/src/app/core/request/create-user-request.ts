import { CreateDependentRequest } from "./create-dependent-request";

export interface CreateUserRequest {
    name: string;
    age: number;
    dependent: CreateDependentRequest;
}