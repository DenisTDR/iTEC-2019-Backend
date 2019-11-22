#!/bin/bash

exec ./set-env-vars.sh "dotnet run --generate-razor-view true --controller $1 --view-names all"
