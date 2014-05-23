﻿(*
A. Baygeldin (c) 2014
ChatBot with WinForms
*)

module ChatBot

open System
open System.Drawing
open System.Windows.Forms
open AIMLbot

let myBot = new Bot()
myBot.loadSettings()
let myUser = new AIMLbot.User(Environment.UserName, myBot);
myBot.loadAIMLFromFiles();

let form = new Form(Text = "AIML Bot", Height = 400, Width = 300)
let message = new TextBox()
let conversation = new TextBox()

let enter (message:#TextBox) (conversation:#TextBox) (e:KeyEventArgs) =
    if (e.KeyCode = Keys.Enter) then 
        let request = new Request(message.Text, myUser, myBot)
        let answer = myBot.Chat(request)
        conversation.AppendText(Environment.UserName + ": " + message.Text + "\n")
        conversation.AppendText("AIML: " + answer.Output.ToString() + "\n")
        message.Clear()

let rnd = new Random()
let r, g, b = rnd.Next(256), rnd.Next(256), rnd.Next(256)

message.Dock <- DockStyle.Bottom
conversation.Dock <- DockStyle.Fill
conversation.Multiline <- true
conversation.WordWrap <- true
conversation.ReadOnly <- true
conversation.BackColor <- Color.FromArgb(r, g, b)

form.Controls.Add(conversation)
form.Controls.Add(message)
form.Show()

message.KeyDown.Add(enter message conversation)
message.KeyDown.Add(fun e -> if e.KeyCode = Keys.Escape then Application.Exit())

Application.Run()