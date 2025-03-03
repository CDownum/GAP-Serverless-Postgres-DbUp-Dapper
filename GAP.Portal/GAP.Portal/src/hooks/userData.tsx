import { Guid } from "guid-typescript";
import { QueryObserverResult, useQuery } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import gapApiClient from "../services/gapApiClient";
import { User } from "../interfaces/User";

const fetchUser = async (companyId: number, id: Guid): Promise<AxiosResponse<User, unknown>> => {
    return await gapApiClient.get<User>(`/api/companies/${companyId}/users/${id}`);
};

export const useFetchUser = (companyId: number, id: Guid): QueryObserverResult<User, unknown> => {
   
    return useQuery<User, unknown>({
        queryFn: async () => {
            const { data } = await fetchUser(companyId, id);
            return data;
        },
        queryKey: [ 'loggedInUser' ]
    });
};