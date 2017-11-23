import urllib.request

print("Insira o código ICAO do aeroporto: ")
icao=input()
icaourl = "http://aviationweather.gov/metar/data?ids=%s&format=raw&date=0&hours=0&taf=on" %icao

maximumCH = 1000
print ("Teste 1: %s" %icaourl)
content = urllib.request.urlopen(icaourl).read()
content = str(content)
find = '<!-- Data starts here -->'
posicao = int(content.index(find) + len(find))
metar = content[ posicao : posicao  + maximumCH]

print("Metar and TAF for %s: " %icao + metar)
with open("output_metar.txt", "w") as text_file:
    text_file.write(icao + metar)

