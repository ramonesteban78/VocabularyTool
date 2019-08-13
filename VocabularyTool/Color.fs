module Color

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms


// Model
type Color =
    | Red
    | Yellow
    | Green
    | Blue
    | Orange
    | Pink
    | Black

type Model = {
    color : Color;
    isSelected : bool
}

// Msg
type Msg = 
    | ColorSelected of Color
    
// Init
let init model = { color = model.color ; isSelected = model.isSelected }, Cmd.none

// Functions
let getColor color =
    match color with
    | Red -> Xamarin.Forms.Color.Red
    | Yellow -> Xamarin.Forms.Color.Gold
    | Green -> Xamarin.Forms.Color.Green
    | Blue -> Xamarin.Forms.Color.Blue
    | Orange -> Xamarin.Forms.Color.Orange
    | Pink -> Xamarin.Forms.Color.Pink
    | Black -> Xamarin.Forms.Color.Black

let view (model: Model) (dispatch:Msg -> unit) = 
    View.Frame(widthRequest = 26.0,
        heightRequest = 26.0,
        cornerRadius = 13.0,
        padding = Thickness(0.0,0.0,0.0,0.0),
        horizontalOptions = LayoutOptions.Center,
        verticalOptions = LayoutOptions.Center,
        backgroundColor = (if model.isSelected 
                        then Xamarin.Forms.Color.Gray 
                        else Xamarin.Forms.Color.Transparent),
        hasShadow = false,
        content = View.Button(widthRequest = 22.0, 
                                heightRequest = 22.0, 
                                cornerRadius = 11,
                                horizontalOptions = LayoutOptions.Center,
                                verticalOptions = LayoutOptions.Center,
                                backgroundColor = getColor model.color,
                                command = fun () -> ColorSelected model.color |> dispatch))



