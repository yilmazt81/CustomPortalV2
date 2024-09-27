import { GetRequest, Post } from "./Apibase";

export async function GetCompanyMailList() {
    const { data } = await GetRequest(`/api/Mail`);
    return data;
};