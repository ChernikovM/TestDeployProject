export enum EStickersActions{
  GetStickersCollection = '[Stickers] Get Stickers Collection',
  GetLabelsCollection = '[Stickers] Get Labels Collection',

  SetCurrentPage = '[Stickers] Set Current Page',
}

export namespace StickersActions{

  export class GetLabelsCollection {
    static readonly type = EStickersActions.GetLabelsCollection;
    constructor() { }
  }

  export class GetStickersCollection {
    static readonly type = EStickersActions.GetStickersCollection;
    constructor() { }
  }

  export class SetCurrentPage {
    static readonly type = EStickersActions.SetCurrentPage;
    constructor(public newPage: number) { }
  }
}
