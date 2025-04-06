import { makeApiRequest } from "@/utils/api";

export const httpAuthenticateUser = async (login) => {
  return await makeApiRequest({
    method: 'POST',
    url: '/auth/login',
    data: login
  });
};