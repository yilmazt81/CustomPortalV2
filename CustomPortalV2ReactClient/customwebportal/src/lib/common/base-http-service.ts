export class BaseHttpService {
  private readonly HEADERS = {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  };

  get = (url: string) =>
    fetch(url).then((response: Response) => response.json());

  getToken = (url: string, token: string) =>
    fetch(url, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token,
      },
    }).then((response: Response) => response.json());

  post = (url: string, body: object) =>
    fetch(url, {
      method: 'POST',
      headers: this.HEADERS,
      body: JSON.stringify(body),
    }).then(response => response.json());

  postToken = (url: string, body: object, token: string) =>
    fetch(url, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token,
      },
      body: JSON.stringify(body),
    }).then(response => response.json());
}