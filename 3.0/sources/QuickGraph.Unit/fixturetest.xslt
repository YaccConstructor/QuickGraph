<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
        <xsl:include href ="common.xslt"/>
        <xsl:template match="//Fixture">
			<html>
				<xsl:comment> saved from url=(0028)http://blog.dotnetwiki.org/ </xsl:comment>
				<head>
                    <title>
                        <xsl:value-of select="@Name"/>,
                        <xsl:call-template name="counter-figures"/>
                    </title>
                    <xsl:call-template name="meta-html" />
                </head>
                <body>
                    <xsl:call-template name="fixture-detail"/>
                    <xsl:call-template name="unit-copyright" />
                </body>
            </html>
        </xsl:template>
    </xsl:stylesheet>