// hooks/useData.ts
import { useCallback } from "react";
import useRequest from "@/hooks/use-request";
import { Method } from "axios";

const BASE_URL = "/api";

const useData = () => {
  const { sendRequest } = useRequest();

  const handleData = useCallback(
    async (
      section: string,
      method: Method,
      body?: any,
      id?: string,
      queryParams?: Record<string, any>
    ) => {
      let url = id
        ? `${BASE_URL}/${section.toLowerCase()}/${id}`
        : `${BASE_URL}/${section.toLowerCase()}`;

      if (queryParams && method === "GET") {
        const queryString = new URLSearchParams(queryParams).toString();
        url = `${url}?${queryString}`;
      }

      console.log(url);

      try {
        const response = await sendRequest({ url, method, body });
        return response;
      } catch (error) {
        console.error(`Error in ${method} request for ${section}:`, error);
        throw error;
      }
    },
    [sendRequest]
  );

  const fetchData = useCallback(
    async (section: string, queryParams?: Record<string, any>) => {
      console.log(section);
      return handleData(section, "GET", undefined, undefined, queryParams);
    },
    [handleData]
  );

  const saveData = useCallback(
    async (section: string, data: any, id?: string) => {
      const method = id ? "PUT" : "POST";
      return handleData(section, method, data, id);
    },
    [handleData]
  );

  const deleteData = useCallback(
    async (section: string, id: string) => {
      return handleData(section, "DELETE", undefined, id);
    },
    [handleData]
  );

  return {
    fetchData,
    saveData,
    deleteData,
  };
};

export default useData;
