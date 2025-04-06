export const base_url = process.env.NEXT_PUBLIC_BASE_URL;

export const httpConfig = {
    mode: 'no-cors',
    headers: {
        'Access-Control-Allow-Origin': '*'
    }
};

export const tokenConfig = () => ({
    ...httpConfig,
    headers: {
        ...httpConfig.headers,
        Authorization: `Bearer ${localStorage.token}`,
    },
});
