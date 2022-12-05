import { AgirlikBirims } from "./enum/AgirlikBirims";
import { MiktarBirims } from "./enum/MiktarBirims";
import { SiparisDurums } from "./enum/SiparisDurums";

export class Siparis {
    musteriSiparisNo:string|undefined;
    cikisAdresi :string|undefined;
    varisAdresi: string|undefined;
    miktar: number|undefined;
    miktarBirim :MiktarBirims| undefined;
    agirlik:number|undefined;
    agirlikBirim :AgirlikBirims|undefined;
    malzemeKodu : string|undefined;
    not: string|undefined;
    siparisDurum :SiparisDurums|undefined;
}