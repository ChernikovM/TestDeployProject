import {Label} from "./label";

export class StickerPack {
  public id?: number;
  public count?: number;
  public labels?: Label[];
  public name?: string;
  public telegramLink?: string;
  public isRemoved?: boolean;
}
