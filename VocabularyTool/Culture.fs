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
    View.ImageButton(widthRequest = 32.0, 
        heightRequest = 32.0, 
        source = model.flag,
        horizontalOptions = LayoutOptions.Center,
        verticalOptions = LayoutOptions.Center,
        backgroundColor = (if model.isSelected 
                        then Xamarin.Forms.Color.Gray 
                        else Xamarin.Forms.Color.Transparent),
        command = fun () -> CultureSelected model |> dispatch)