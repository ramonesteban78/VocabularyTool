module Culture

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

type Model = {
    name : string;
    flag : string;
    isSelected : bool
}

type Msg = 
    | CultureSelected of Model


let init model = 
    { name = model.name; flag = model.flag; isSelected = model.isSelected }, Cmd.none


let view (model: Model) (dispatch:Msg -> unit) = 
    View.ImageButton(widthRequest = (if model.isSelected then 42.0 else 32.0), 
        heightRequest = (if model.isSelected then 42.0 else 32.0), 
        source = model.flag,
        horizontalOptions = LayoutOptions.Center,
        verticalOptions = LayoutOptions.Center,
        command = fun () -> CultureSelected model |> dispatch)