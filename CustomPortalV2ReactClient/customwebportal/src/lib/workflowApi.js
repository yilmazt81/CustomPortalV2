import { GetRequest, Post } from "./Apibase";

export async function GetBranchWorkFlows() {
    const { data } = await GetRequest(`/api/Workflow`);
    return data;
};